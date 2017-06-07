import { FormStateMap } from "redux-form"

export type Job = {
    id: string,
    name: string
}

export type AppState = {
    form: FormStateMap,
    job?: Job,
}

export type DispatchProps = {
    dispatch: Function,
}