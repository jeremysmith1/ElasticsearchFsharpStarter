This project demos pushing data into elasticsearch with F#. Included with starter code, there is also a docker-compose for setting up a cluster quickly.

Dependencies:
docker-compose
dotnet core


To run, start the cluster using "docker-compose up"
To stop, "docker-compose down"
To stop and delete data "docker-compose down -v"

Once up, Elasticsearch is here: http://127.0.0.1:9200/
And Kibana is here: http://127.0.0.1:5601/

To modify data, start in Program.fs to modify data

To run, use the dotnet cli https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet?tabs=netcore2x

In this case "dotnet run"

You should recieve a message of the result. Create an index pattern in Kibana, then view your data.