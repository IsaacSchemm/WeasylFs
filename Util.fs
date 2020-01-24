namespace WeasylFs

open System
open System.Net
open System.IO
open FSharp.Json

module Util =
    /// The default Weasyl URL, used when the IWeasylCredentials object does not also implement IAlternateWeasylUri.
    let DefaultWeasylUri = new Uri("https://www.weasyl.com")

    /// Creates an HttpWebRequest object (with Accept: application/json), given a set of Weasyl credentials and a relative URL.
    let CreateRequest (credentials: IWeasylCredentials) (path: string) =
        let baseUri =
            match credentials with
            | :? IAlternateWeasylUri as a -> a.Uri
            | _ -> DefaultWeasylUri
        let req =
            new Uri(baseUri, path)
            |> WebRequest.CreateHttp
        req.Headers.Add("Accept", "application/json")
        if not (isNull credentials.ApiKey) then
            req.Headers.Add("X-Weasyl-API-Key", credentials.ApiKey)
        req

    /// An asynchronous computation that executes an HTTP request and deserializes the response as JSON.
    let AsyncReadJson<'a> (req: WebRequest) = async {
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return Json.deserialize<'a> json
    }

    /// Builds a URL-encoded query string from a set of key/value pairs.
    let BuildQueryString (parameters: (string * string) seq) =
        parameters
        |> Seq.map (fun (k, v) -> WebUtility.UrlEncode k, WebUtility.UrlEncode v)
        |> Seq.map (fun (k, v) -> sprintf "%s=%s" k v)
        |> String.concat "&"