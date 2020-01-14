namespace WeasylFs.Endpoints

open WeasylFs
open System.IO

module Version =
    let AsyncExecute credentials = async {
        let req = Shared.createRequest credentials "/api/version"
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        return json
    }