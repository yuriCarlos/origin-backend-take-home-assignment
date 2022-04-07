# Instructions to run the project

Make sure you have docker installed in order to run the project. After that you can run the codes listed below in the project root directory.

```
docker build -t risk-profile-calculator .
docker run -p 5000:80 risk-profile-calculator
```

The endpoint asked on the test description is in the path POST `localhost:5000/calculate-risk`, so when testing the app redirect your calls to this endpoint. You also can check the `localhost:5000/swagger`. In this page it is possible to run calls and see an automated documentation.

# Architecture

The application was implemented using an Api layer to handle http calls, an Application layer to decouple the use cases from the interfaces that will activate the use cases (whether called http, gRPC or queue) and a domain layer to define domain concepts. The project structure follows the following format:

```
| - src
|   | - RiskProfile.Api                     # api project
|   | - RiskProfile.Domain                  # domain layer project
|   | - RiskProfileCalculator.Application   # application layer project
```

I also defined a project called `RiskProfile.CalculatorEngine` to separate what I've created to compose the calculation engine.

# Calculation engine

The calculation engine implemented is quite simple and is based entirely on the creation and chaining of calculators. To create a new calculator just create an instance of `RiskCalculatorBuilder<T>` and compose it by defining the calculators that are part of the construction process of the calculator you are defining. Here's an example of how to create a new calculator:

```
var calculatorBuilder = new RiskScoreCalculator<PersonalProfile>();

var calculator = calculatorBuilder
    .StartBuilding()                                            // Start the build process
    .AddCalculator(personalProfile => true, new RiskScore(1))   // Add a calculator definition
    .Build();                                                   // Build a calculator

var riskScore = calculator.Calculate(somePersonProfile);        // Calculates the score
```

There are two ways to add calculators to the build process. In the first one you add a predicate and its weight. For example:
```
var calculatorBuilder = new RiskScoreCalculator<PersonalProfile>();
calculatorBuilder
    .StartBuilding()                                            
    .AddCalculator(personalProfile => true, new RiskScore(1))   // Add a calculator definition
    .Build();
```
In the second one you add an ``IRiskScoreCalculator<T>``. For example:

```
var calculatorBuilder = new RiskScoreCalculator<PersonalProfile>();

var calculator = calculatorBuilder
    .StartBuilding()                        // Start the build process
    .AddCalculator(someCalculator)          // Add a calculator definition
    .Build();        
```

You can also chain one calculator to another this way:

```
var calculatorBuilder = new RiskScoreCalculator<PersonalProfile>();

var calculator1 = calculatorBuilder
    .StartBuilding()
    .AddCalculator(personalProfile => true, new RiskScore(1))
    .Build();

var calculatorWithOtherCalculator = calculatorBuilder
    .StartBuilding()
    .AddCalculator(calculator1) // <----------- Chaining the calculator
    .Build();
```
That way calculator ``calculatorWithOtherCalculator`` will use ``calculator1`` during its risk calculations.

You can see an example of using this feature in the ``GeneralRiskScoreCalculator`` class, where a calculator with rules that affect all insurance calculators is defined. This general calculator is chained to all services that calculate insurance risk. Below is a copy of one of those services:

``CalculateAutoInsuranceRiskService``:
```
public CalculateAutoInsuranceRiskService(IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder,
            INewVehicleSpecification newVehicleSpecification,
            IGeneralRiskScoreCalculator<PersonalProfile> generalRiskScoreCalculator)
        {
            _scoreCalculator = riskScoreCalculatorBuilder
                .StartBuilding()
                .AddCalculator(generalRiskScoreCalculator) // <----- chaining the calculator
                .AddCalculator(personalProfile => personalProfile.Vehicle == null, RiskScore.CreateIneligibleRiskScore())
                .AddCalculator(personalProfile => newVehicleSpecification.IsANewVehicle(personalProfile.Vehicle), new RiskScore(1))
                .Build();
        }
```

# Domain layer

According to the problem statement, we can see that the risk calculation of each insurance is based on a decision case and that each insurance gives the weight that it wants for each one of these cases (in the case of a decision if a person is married or not for example, a weight of 1 is given for life insurance and a weight of -1 is given for disability insurance).

These decision cases define domain concepts that have their own rules. Although the definition of these rules is quite simple (like a single boolean comparison), these concepts could evolve in the future and need more rules to be expressed. So I chose to implement ``Specifications`` that expressed these domain concepts and could be reused where needed. These implementations can be found in the ``Spescifications`` folder of the domain project.

As each insurance defines its risk rules and the weight of each rule, I chose to implement domain services that would do these definitions and use the calculators to calculate the risk. These services are located in the ``Services`` folder in domain project.

# Application layer

Even though the problem proposed in the statement is quite simple, I chose to define an application layer, to show that, if one day these use cases need to be used by another means of communication, we could reuse all the code, modifying only the presentation layer.

The concept of outputport was also used to return the results, because I find it easier to define different usecase return types.

# Tests

I've implemented only unit tests even though I know different types of test. These testes are under the folder `./src/RiskProfile.UnitTests`.

# Questions

In case of any doubt, please do not hesitate to contact me.

Yuri Carlos Bonifácio Neves

Email: yuri.bonifacio.neves@gmail.com

