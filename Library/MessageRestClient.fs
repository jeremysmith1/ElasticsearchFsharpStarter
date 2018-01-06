module MessageRestClient

open RestSharp

let AlphaAdvClient = RestClient("https://www.alphavantage.co")

type Message = string * AsyncReplyChannel<string>

type MessageBasedRestClient () =

   static let agent = MailboxProcessor<Message>.Start(fun inbox ->
    
        let rec requestLoop = async {

            let! (queryString, replyChannel) = inbox.Receive()

            let request = RestRequest queryString

            let response = AlphaAdvClient.Execute request

            replyChannel.Reply(response.Content)

            //wait 1.5 sec before next request
            do! Async.Sleep(1500)

            do! requestLoop
        }

        requestLoop
   )
   static member MakeRequest i = agent.PostAndAsyncReply(fun replyChannel -> i, replyChannel)
