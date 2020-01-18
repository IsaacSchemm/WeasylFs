namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json
open System

module FrontPage =
    type Request() =
        member val Since = Nullable<DateTimeOffset>() with get, set
        member val Count = Nullable<int>() with get, set

    let AsyncExecute credentials (parameters: Request) = async {
        let query = seq {
            if parameters.Since.HasValue then
                yield ("since", parameters.Since.Value.ToString("o"))
            if parameters.Count.HasValue then
                yield ("count", parameters.Count.Value.ToString())
        }
        let qs = Shared.buildQueryString query
        let url = sprintf "/api/submissions/frontpage?%s" qs 
        let req = Shared.createRequest credentials url
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<Submission list> json
    }

    let ExecuteAsync credentials parameters =
        AsyncExecute credentials parameters
        |> Async.StartAsTask