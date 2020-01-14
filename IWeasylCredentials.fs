namespace WeasylFs

type IWeasylCredentials =
    abstract member ApiKey: string with get

type WeasylCredentials = {
    apiKey: string
} with
    interface IWeasylCredentials with
        member this.ApiKey = this.apiKey