namespace WeasylFs.Endpoints

open WeasylFs

module Version =
    type Response = {
        short_sha: string
    }

    let AsyncExecute credentials =
        "api/version"
        |> Util.CreateRequest credentials
        |> Util.AsyncReadJson<Response>

    let ExecuteAsync credentials =
        AsyncExecute credentials
        |> Async.StartAsTask