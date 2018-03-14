open ElasticWrapper
open AlphaAdv
open System
open ESCurrencyModel

[<EntryPoint>]
let main argv =
    //Retrieve all Currency symbols we can collect (currency code,currency name)
    let currencySymbols = 
        IO.File.ReadLines(@".\Resource\CurrencySymbols.csv") 
        |> Seq.skip 1 //Skip column labels
        |> Seq.map ((fun (line: string) -> line.Split ',') 
        >> (fun (values: string[]) -> (values.[0], values.[1]))) 
    
    for request in (BuildRequestFromCurrenceySymbols currencySymbols) do
        DigitalCurrencyRequest request 
        |> TryParseDatum 
        |> convertToESDatum request.symbol
        |> BulkInsertWithGivenIndex request.indexName
        |> Console.WriteLine


    // for file in BackupPlan.JsonDataCollection do
    //     convertResponse file
    //     |> convertToESDatum "backup"
    //     |> BulkInsertWithGivenIndex "backup"
    //     |> Console.WriteLine

    0 // return an integer exit code