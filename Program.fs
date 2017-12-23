open ElasticWrapper
open Person

[<EntryPoint>]
let main argv =

    let testPerson = {Id=7; FirstName="Porky"; LastName="Pig"}

    let client = elasticsearchClientDefault

    match insertPerson client testPerson with
    | true -> printfn "Successfully Added:\n %A" testPerson
    | false -> printfn "Could Not Add: %A" testPerson

    0 // return an integer exit code