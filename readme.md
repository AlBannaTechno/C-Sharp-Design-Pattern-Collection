#### Design Pattern Implementations In C# 8

This projects contains most of object oriented design patterns implemented in C# 8
`This is an update of the old/legacy project [removed]`

 Notes: Both **Concurrency patterns**, **Functional Patterns** does not implemented here `yet`
 
 #### For more details 
   * [Software design pattern](https://en.wikipedia.org/wiki/Software_design_pattern)
   * [Design Patterns](https://en.wikipedia.org/wiki/Functional_design)
 
##### Keys to understand this projects and get most benefits
* you should familiar with advanced and most recent c# 8 features
    * you may need some C++/17+ knowledge in some patterns implementations
* You should read at least one book about design pattern and solid principles
    * if you main C++ developer, you must start with [Design Patterns: Elements of Reusable Object-Oriented Software ](https://www.amazon.com/Design-Patterns-Elements-Reusable-Object-Oriented/dp/0201633612)
         * you will need to know legacy C++ code, si if you just miss with C++ start from C++11, you may find some strage code, since many thing changed
         , but i prefere to study old [c++3 specifications](https://en.wikipedia.org/wiki/C%2B%2B03) [cppreference](https://en.cppreference.com/w/cpp/language/history) and then read this book, just notice cppreference altered c++98 and c++03 due to legacy code base and buggy implementations, but there is some resources
            * [DDS-PSM-Cxx - specifications c++2003](https://www.omg.org/spec/DDS-PSM-Cxx/About-DDS-PSM-Cxx/)
            * [GCC GNU, only implementation status](https://gcc.gnu.org/onlinedocs/libstdc++/manual/status.html#status.iso.1998)
            * [STD, both specifications and implementation, The Best](http://www.open-std.org/jtc1/sc22/wg21/docs/papers/2013/n3690.pdf)
         
    * if you a pure C# developer: [Dofactory-Design-Patterns](https://www.dofactory.com/net/design-patterns)
    
    
* You must read the source code in ordered way
    * For Patterns we will explains the order below
    * For patterns implementations every files should start with {C}\_\[N\]\_description
        * You must track this order :
        * For example : `A_1_VectorRasterNoCaching.cs` comes before `A_2_VectorRasterWithCaching.cs` in adapter pattern
* Every Pattern Directory should have *.md file to describe that pattern `Not Completed Yet`
    
    
#### Order
    
* Solid Principles : `may not contains any implementation`
    * SingleResponsibility
    * OpenClosed
    * LiskovSubstitution
    * InterfaceSegregation
    * DependencyInversion
    
* Creational Patterns
    * Builder
    * Factories
    * Prototype
    * Singleton
* Structural Patterns
    * Adapter
    * Bridge
    * Composite
    * Decorator
    * Facade
    * FlyWight
    * Proxy
* Behavioural Patterns
    * Chain Of Responsibility
    * Command
    * Interpreter
    * Iterator
    * Mediator
    * Memento
    * Null Object
    * Observer
    * State
    * Strategy
    * Template Method
    * Visitor
    
#### Since this project is a replacement of the old project this not completed yet, so you may found shortage in descriptions
with some not-completed examples, but i think by `Nov 2020` the update will be completed
   
#### Also notice , C++ Design patterns repository also removed , until i update it to C++ 20 i will not publish it , may published by `Dec 2020`
