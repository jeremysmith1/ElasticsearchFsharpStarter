module ElasticWrapper

open System
open Nest
open ESCurrencyModel

let connectionSettingsDefault index= 
    let settings = new ConnectionSettings(Uri <| "http://127.0.0.1:9200/")
    settings.DefaultIndex(index)

let elasticsearchClientDefault index = 
    ElasticClient (connectionSettingsDefault index)

let InsertCrypto (client:ElasticClient) (person:Datum) : bool =
    let result = client.Index person
    result.IsValid

let BulkInsertCrypto (client:ElasticClient) (data: seq<Datum>) = 
    let descriptor = BulkDescriptor()
    descriptor.IndexMany data |> ignore
    let result = client.Bulk(descriptor)
    result.IsValid