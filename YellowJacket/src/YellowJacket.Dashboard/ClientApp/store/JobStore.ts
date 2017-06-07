// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

import { fetch, addTask } from "domain-task";
import { Action, Reducer, ActionCreator } from "redux";
import { IAppThunkAction } from "./";
import { IJob } from "ClientApp/models/Models";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface IJobState {
    isLoading: boolean;
    payload: any;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface ICreateJobAction {
    type: "CREATE_JOB",
    payload: any;
}

interface ICreateJobSuccessAction {
    type: "CREATE_JOB_SUCCESS",
    payload: any;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = ICreateJobAction | ICreateJobSuccessAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    createJob: (): IAppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
            let fetchTask = fetch("/api/v1/job/")
                .then(response => response.json() as Promise<IJob>)
                .then(data => {
                    dispatch((({ type: "CREATE_JOB_SUCCESS", payload: data })) as any);
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: "CREATE_JOB", payload: {} });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: IJobState = { payload: {}, isLoading: false };

export const reducer: Reducer<IJobState> = (state: IJobState, action: KnownAction) => {
    switch (action.type) {
        case "CREATE_JOB":
        return {
            payload: state.payload,
            isLoading: true
        };
        case "CREATE_JOB_SUCCESS":
        // Only accept the incoming data if it matches the most recent request. This ensures we correctly
        // handle out-of-order responses.
            return {
                payload: action.payload,
                isLoading: false
            };
        default:
        // The following line guarantees that every action in the KnownAction union has been covered by a case above
        const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};