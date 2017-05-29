import * as AgentStore from "./AgentStore";

import { createStore, combineReducers } from "redux";
import { reducer as formReducer } from "redux-form";

// The top-level state object
export interface IApplicationState {
    agents: AgentStore.IAgentState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    agents: AgentStore.reducer,
    form: formReducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface IAppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}