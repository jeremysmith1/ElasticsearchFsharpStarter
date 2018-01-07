module ElasticWrapper

open System
open Nest
open ESCurrencyModel

let connectionSettingsDefault =
    fun index -> 
    let settings = new ConnectionSettings(Uri <| "http://10.0.75.1:9200/")
    settings.DefaultIndex(index)

let elasticsearchClientDefault = 
    fun index -> ElasticClient (connectionSettingsDefault index)

let BulkInsertCrypto = 
    fun ((client:ElasticClient), (data: seq<Datum>)) ->
    let descriptor = BulkDescriptor()
    descriptor.IndexMany data |> ignore
    let result = client.Bulk(descriptor)
    result.IsValid

let BulkInsertWithGivenIndex (symbolString: string) = 
    fun cryptoModel ->
    match cryptoModel with
     | None -> sprintf "Could not add, request unsuccessful: %A" symbolString 
     | Some obj -> 
        let indexName = elasticsearchClientDefault ("crypto_" + symbolString.ToLower())
        if BulkInsertCrypto (indexName, obj) 
        then sprintf "Added: %A" symbolString 
        else sprintf "Could not Index: %A, Check Cluster" symbolString