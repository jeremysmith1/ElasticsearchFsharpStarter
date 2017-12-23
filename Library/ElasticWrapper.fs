module ElasticWrapper

open System
open Nest
open Person

let connectionSettingsDefault = 
    let settings = new ConnectionSettings(Uri <| "http://127.0.0.1:9200/")
    settings.DefaultIndex("people")

let elasticsearchClientDefault = 
    ElasticClient connectionSettingsDefault

let insertPerson (client:ElasticClient) (person:Person) : bool =
    let result = client.Index person
    result.IsValid