using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;

        m_SpawnerObstacle = GetComponent<SpawnerObstacle>();
    }

    [Header("Player")]
    public GameObject m_FishPrefab;
    public Vector3 m_Position = new Vector3(-6, 0, 0);

    [Header("Obstacles")]
    public float m_RepeatRate = 1.65f;
    public float m_Speed = 2.0f;
    public float m_Delay = 1.0f;

    [Header("Genetic Algorithm")]
    public int m_PopulationSize = 50;
    public float m_GenerationTime = 60.0f;
    public int m_MaxGenerations = 5000;
    [Range(0.0f, 1.0f)]
    public float m_MutationRate = 0.02f;
    [Range(0.0f, 1.0f)]
    public float m_Alpha = 0.25f;
    [Range(2, 5)]
    public int m_SelectionNumber = 2;
    [Range(0.0f, 1.0f)]
    public float m_ElitismNumber = 0.1f; 
    public string m_FileName = "generations.xls";
    [HideInInspector]
    public List<Brain> m_Population = new List<Brain>();

    [Header("UI")]
    public Text m_AreaText;

    private int m_Generation = 1;
    private SpawnerObstacle m_SpawnerObstacle;

    public Transform Obstacle { get; set; }
    public float ElapsedTime { get; set; }
    public int Survivors { get; set; }
    public float MaxFitness { get; set; }

    private bool m_IsPlaying;

    public Brain CreateFish()
    {
        var fish = Instantiate(m_FishPrefab, m_Position, Quaternion.Euler(0, 90, 0));
        var brain = fish.GetComponent<Brain>();
        
        return brain;
    }

    public Brain CreateFishWithRandomWeights()
    {
        var brain = CreateFish();
        for (int j = 0; j < brain.m_Chromosome.Length; j++)
            brain.m_Chromosome[j] = Helper.Random();
        return brain;
    }

    public Brain CreateFishByWeights(float[] weights = null)
    {
        var brain = CreateFish();
        System.Array.Copy(weights, brain.m_Chromosome, weights.Length);
        return brain;
    }

    private void InitializeRandomPopulation()
    {
        for (int i = 0; i < m_PopulationSize; i++)
            m_Population.Add(CreateFishWithRandomWeights());
    }

    public float[] Mutate(float[] weights, float mutationRate)
    {
        for (int i = 0; i < weights.Length; i++)
            weights[i] = Helper.Random() < mutationRate ? Helper.Random() : weights[i];

        return weights;
    }

    public float[] Crossover(float[] parent1, float[] parent2)
    {
        float[] offspring = new float[parent1.Length];
        for (int i = 0; i < offspring.Length; i++)
            offspring[i] = parent1[i] + m_Alpha * (parent2[i] - parent1[i]);

        return offspring;
    }

    public Brain TournamentSelection()
    {
        List<Brain> brains = new List<Brain>();
        for (int i = 0; i < m_SelectionNumber; i++)
        {
            int index = Helper.Random(0, m_PopulationSize);
            brains.Add(m_Population[index]);
        }

        return brains.OrderByDescending(x => x.m_Fitness).ToList()[0];
    }

    public List<Brain> Elitism()
    {
        List<Brain> elite = new List<Brain>();
        var candidates = m_Population.OrderByDescending(x => x.m_Fitness).ToList();
        var elitism = (int)(m_PopulationSize * m_ElitismNumber);
        for (int i = 0; i < elitism; i++)
            elite.Add(CreateFishByWeights(candidates[i].m_Chromosome));

        return elite;
    }

    public void NewPopulation()
    {
        List<Brain> newPopulation = Elitism();

        while (newPopulation.Count < m_PopulationSize)
        {
            var parent1 = TournamentSelection();
            var parent2 = TournamentSelection();

            var offspring1 = Crossover(parent1.m_Chromosome, parent2.m_Chromosome);
            offspring1 = Mutate(offspring1, m_MutationRate);
            newPopulation.Add(CreateFishByWeights(offspring1));

            if (newPopulation.Count < m_PopulationSize)
            {
                var offspring2 = Crossover(parent2.m_Chromosome, parent1.m_Chromosome);
                offspring2 = Mutate(offspring2, m_MutationRate);
                newPopulation.Add(CreateFishByWeights(offspring2));
            }
        }

        MaxFitness = Mathf.Max(MaxFitness, m_Population.Max(x => x.m_Fitness));

        Save(m_Generation != 1);

        KillAll();

        m_Generation++;
        m_Population = newPopulation;
    }

    private void KillAll()
    {
        foreach (Brain brain in m_Population)
            Destroy(brain.gameObject);

        m_Population.Clear();
    }

    private void UpdateNearObstacle()
    {
        Obstacle = null;
        foreach (var go in m_SpawnerObstacle.ActiveObstacles)
        {
            if (go.transform.position.x > m_Position.x)
            {
                float distance = Vector3.Distance(go.transform.position, m_Position);
                if (Obstacle == null || distance < Vector3.Distance(Obstacle.position, m_Position))
                    Obstacle = go.transform;
            }
        }

        if (Obstacle == null)
            Obstacle = m_SpawnerObstacle.m_TopLimiter;    }

    private void Update()
    {
        if (m_IsPlaying)
        {
            UpdateNearObstacle();
            ElapsedTime += Time.deltaTime;
            UpdateUI();

            if (ElapsedTime >= m_GenerationTime || Survivors <= 0)
            {
                StopSpawner();
                NewPopulation();

                if (m_Generation > m_MaxGenerations)
                    Application.Quit();

                Restart();
            }
        } 
        else
        {
            m_AreaText.text = "Press ENTER";

            if (Input.anyKeyDown)
            {
                m_IsPlaying = true;
                InitializeRandomPopulation();
                Restart();
            }
        }
    }

    private void SpawnObstacle()
    {
        m_SpawnerObstacle.Spawn(m_Speed);
    }

    public void Restart()
    {
        Survivors = m_PopulationSize;
        ElapsedTime = 0.0f;
        InvokeRepeating("SpawnObstacle", m_Delay, m_RepeatRate);
    }

    public void StopSpawner()
    {
        CancelInvoke("SpawnObstacle");
        m_SpawnerObstacle.DisableAll();
    }

    public void UpdateUI()
    {
        m_AreaText.text = "";
        m_AreaText.text += $"Generation: {m_Generation}\n";
        m_AreaText.text += $"Best Fitness: {MaxFitness:0.0}\n";
        m_AreaText.text += $"Survivors: {Survivors} / {m_Population.Count}\n\n";
        m_AreaText.text += $"Elapsed Time: {ElapsedTime:0} / {m_GenerationTime:0}\n";
    }

    public void Save(bool append)
    {
        var directory = $"{Application.dataPath}/../Save";
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        if (string.IsNullOrEmpty(m_FileName))
        {
            return;
        }

        using (StreamWriter file = new StreamWriter($"{directory}/{m_FileName}", append))
        {
            float avg = m_Population.Average(x => x.m_Fitness);
            float min = m_Population.Max(x => x.m_Fitness);
            float max = m_Population.Max(x => x.m_Fitness);
            file.WriteLine($"{min}\t{avg}\t{max}");
        }
    }
}