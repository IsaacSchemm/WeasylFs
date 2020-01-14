namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json

module Whoami =
    type Response = {
        login: string
        userid: int
    }

    let AsyncExecute credentials = async {
        let req = Shared.createRequest credentials "/api/whoami"
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<Response> json
    }

    let ExecuteAsync credentials =
        AsyncExecute credentials
        |> Async.StartAsTask