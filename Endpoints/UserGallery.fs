namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json
open System
open System.Net

module UserGallery =
    type Request() =
        member val Since = Nullable<DateTimeOffset>() with get, set
        member val Count = Nullable<int>() with get, set
        member val FolderId = Nullable<int>() with get, set
        member val BackId = Nullable<int>() with get, set
        member val NextId = Nullable<int>() with get, set

    type Response = {
        submissions: Submission list
        backid: int option
        nextid: int option
    } with
        member this.NullableBackId =
            this.backid
            |> Option.toNullable
        member this.NullableNextId =
            this.nextid
            |> Option.toNullable

    let AsyncExecute credentials (parameters: Request) username = async {
        let query = seq {
            if parameters.Since.HasValue then
                yield ("since", parameters.Since.Value.ToString("o"))
            if parameters.Count.HasValue then
                yield ("count", parameters.Count.Value.ToString())
            if parameters.FolderId.HasValue then
                yield ("folderid", parameters.FolderId.Value.ToString())
            if parameters.BackId.HasValue then
                yield ("backid", parameters.BackId.Value.ToString())
            if parameters.NextId.HasValue then
                yield ("nextid", parameters.NextId.Value.ToString())
        }
        let qs = Shared.buildQueryString query
        let url = sprintf "/api/users/%s/gallery?%s" (WebUtility.UrlEncode username) qs
        let req = Shared.createRequest credentials url
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<Response> json
    }

    let ExecuteAsync credentials parameters username =
        AsyncExecute credentials parameters username
        |> Async.StartAsTask