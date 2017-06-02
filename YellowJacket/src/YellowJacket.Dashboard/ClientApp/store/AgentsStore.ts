import { fetch, addTask } from "domain-task";
import { Action, Reducer, ActionCreator } from "redux";
import { IAppThunkAction } from "./";
import { IAgent } from "ClientApp/models/Models";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface IAgentsState {
    isLoading: boolean;
    agents: IAgent[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface IRequestAgentsAction {
    type: "REQUEST_AGENTS",
}

interface IReceiveAgentsAction {
    type: "RECEIVE_AGENTS",
    agents: IAgent[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = IRequestAgentsAction | IReceiveAgentsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAgents: (): IAppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
            let fetchTask = fetch("/api/v1/agent/")
                .then(response => response.json() as Promise<IAgent[]>)
                .then(data => {
                    dispatch((({ type: "RECEIVE_AGENTS", agents: data })) as any);
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: "REQUEST_AGENTS" });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: IAgentsState = { agents: [], isLoading: false };

export const reducer: Reducer<IAgentsState> = (state: IAgentsState, action: KnownAction) => {
    switch (action.type) {
        case "REQUEST_AGENTS":
        return {
            agents: state.agents,
            isLoading: true
        };
        case "RECEIVE_AGENTS":
        // Only accept the incoming data if it matches the most recent request. This ensures we correctly
        // handle out-of-order responses.
            return {
                agents: action.agents,
                isLoading: false
            };
        default:
        // The following line guarantees that every action in the KnownAction union has been covered by a case above
        const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};