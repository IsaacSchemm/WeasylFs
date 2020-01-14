namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json
open System

module UserAvatar =
    type Response = {
        avatar: string
    }

    let AsyncExecute credentials username = async {
        let req =
            username
            |> Uri.EscapeUriString
            |> sprintf "/api/useravatar?username=%s"
            |> Shared.createRequest credentials
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<Response> json
    }

    let ExecuteAsync credentials username =
        AsyncExecute credentials username
        |> Async.StartAsTask