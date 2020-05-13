# Genetic Algorithm - Happy Fish

Example of application of genetic algorithm for evolution flappy bird controller. Project developed in the discipline of Artificial Intelligence applied to the Digital Games - Fatec Americana

## Happy Fish (Flappy Bird based game)

Happy Fish is a casual game where you control a fish and have to guide him through the obstacles. Tap the screen to make the fish swim, in order to dodge all these obstacles.

<p align="center">
  <img src="https://github.com/kleberandrade/evolve-happy-fish-unity/blob/master/Figures/game.png" width="600"/>
</p>

Happy Fish game developed at Unity 2019.3.10

-   Mobile game: [https://play.google.com/store/apps/details?id=com.kleberandrade.happyfish](https://play.google.com/store/apps/details?id=com.kleberandrade.happyfish)

## Genetic Algorithm (GA)

<p align="center">
  <img src="https://github.com/kleberandrade/evolve-happy-fish-unity/blob/master/Figures/algorithm.png" width="800"/>
</p>

### Chromosome

<p align="center">
  <img src="https://github.com/kleberandrade/evolve-happy-fish-unity/blob/master/Figures/brain.png" width="800"/>
</p>

### Fitness Function

Fitness Function (also known as the Evaluation Function) evaluates how close a given solution is to the optimum solution of the desired problem.

-   We use the lifetime of the fish

### Elitism

A practical variant of the general process of constructing a new population is to allow the best organism(s) from the current generation to carry over to the next, unaltered. This strategy is known as elitist selection and guarantees that the solution quality obtained by the GA will not decrease from one generation to the next.

### Tournament Selection

Tournament Selection is a Selection Strategy used for selecting the fittest candidates from the current generation in a Genetic Algorithm. These selected candidates are then passed on to the next generation. In a K-way tournament selection, we select k-individuals and run a tournament among them. Only the fittest candidate amongst those selected candidates is chosen and is passed on to the next generation. In this way many such tournaments take place and we have our final selection of candidates who move on to the next generation. It also has a parameter called the selection pressure which is a probabilistic measure of a candidate’s likelihood of participation in a tournament. If the tournament size is larger, weak candidates have a smaller chance of getting selected as it has to compete with a stronger candidate. The selection pressure parameter determines the rate of convergence of the GA. More the selection pressure more will be the Convergence rate. GAs are able to identify optimal or near-optimal solutions over a wide range of selection pressures. Tournament Selection also works for negative fitness values.

1.  Select k individuals from the population and perform a tournament amongst them
2.  Select the best individual from the k individuals
3.  Repeat process 1 and 2 until you have the desired amount of population

### Blend Crossover and Random Mutation

In a uniform crossover, we don’t divide the chromosome into segments, rather we treat each gene separately. In this, we essentially flip a coin for each chromosome to decide whether or not it’ll be included in the off-spring. We can also bias the coin to one parent, to have more genetic material in the child from that parent.

Random mutation is an extension of the bit flip for the integer representation. In this, a random value from the set of permissible values is assigned to a randomly chosen gene.

<p align="center">
  <img src="https://github.com/kleberandrade/evolve-happy-fish-unity/blob/master/Figures/operators.png" width="300"/>
</p>

## Experiments and Results

Initial setup of the experiment

| Variable         	              | Value |
|------------------	              |:-----:|
| Population size  	              |  20   |
| Elitism          	              |  10%   |
| Blend Crossover    	          |  0,2  |
| Mutation rate    	              |   2%  |
| Tournament size  	              |   2 indivi  |
| Trial time      	              |   100 seconds |

Video: [https://www.youtube.com/watch?v=bmvs9ugStgg](https://www.youtube.com/watch?v=bmvs9ugStgg)

## Acknowledgment

Thanks to Professor Marcio Crocomo for the ideas of the evolution strategy

## Licença

    Copyright 2020 Kleber de Oliveira Andrade
    
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
