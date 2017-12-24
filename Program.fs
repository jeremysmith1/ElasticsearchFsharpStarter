open ElasticWrapper
open Person
open AlphaAdv

[<EntryPoint>]
let main argv =

    let values = { nameOfFunction= FunctionTypes.Daily; symbol= SymbolTypes.Bitcoin; market= MarketTypes.UnitedStatesDollar; }

    let response = DigitalCurrencyRequest values

    let currencyClient = elasticsearchClientDefault "bitcoin"

    let testPerson = {Id=7; FirstName="Porky"; LastName="Pig"}

    let peopleClient = elasticsearchClientDefault "people"

    match insertPerson peopleClient testPerson with
    | true -> printfn "Successfully Added:\n %A" testPerson
    | false -> printfn "Could Not Add: %A" testPerson

    0 // return an integer exit code