namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json

module Version =
    type Response = {
        short_sha: string
    }

    let AsyncExecute credentials = async {
        let req = Shared.createRequest credentials "/api/version"
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<Response> json
    }

    let ExecuteAsync credentials =
        AsyncExecute credentials
        |> Async.StartAsTask