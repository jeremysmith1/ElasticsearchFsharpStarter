module ElasticWrapper

open System
open Nest
open Person

let connectionSettingsDefault index= 
    let settings = new ConnectionSettings(Uri <| "http://127.0.0.1:9200/")
    settings.DefaultIndex(index)

let elasticsearchClientDefault index = 
    ElasticClient (connectionSettingsDefault index)

let insertPerson (client:ElasticClient) (person:Person) : bool =
    let result = client.Index person
    result.IsValid