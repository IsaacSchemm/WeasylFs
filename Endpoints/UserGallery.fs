namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json
open System
open System.Net

module UserGallery =
    type Request = {
        since: DateTimeOffset option
        count: int option
        folderid: int option
        backid: int option
        nextid: int option
    }

    let DefaultRequest = {
        since = None
        count = None
        folderid = None
        backid = None
        nextid = None
    }

    type Response = {
        submissions: Submission list
        backid: int option
        nextid: int option
    }

    let AsyncExecute credentials parameters username = async {
        let qs =
            seq {
                match parameters.since with
                | Some s -> yield ("since", s.ToString "o")
                | None -> ()
                match parameters.count with
                | Some s -> yield ("count", sprintf "%d" s)
                | None -> ()
                match parameters.folderid with
                | Some s -> yield ("folderid", sprintf "%d" s)
                | None -> ()
                match parameters.backid with
                | Some s -> yield ("backid", sprintf "%d" s)
                | None -> ()
                match parameters.nextid with
                | Some s -> yield ("nextid", sprintf "%d" s)
                | None -> ()
            }
            |> Seq.map (fun (k, v) -> sprintf "%s=%s" (WebUtility.UrlEncode k) (WebUtility.UrlEncode v))
            |> String.concat "&"
        let req =
            sprintf "/api/users/%s/gallery?%s" (WebUtility.UrlEncode username) qs
            |> Shared.createRequest credentials
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<Response> json
    }

    let ExecuteAsync credentials parameters username =
        AsyncExecute credentials parameters username
        |> Async.StartAsTask