import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';
import * as axios from 'axios';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface IJobsState {
    isLoading: boolean;
    agents: IJob[];
}

export interface IJob {
    id: string;
    name: string;
}

export interface IOperationFailedJob {
    httpCode: string;
    message: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

//interface IRequestJobsAction {
//    type: 'REQUEST_JOBS';
//}

//interface IReceiveJobsAction {
//    type: 'RECEIVE_JOBS';
//    jobs: IJob[];
//}

//interface IRequestJobAction {
//    type: 'REQUEST_JOB';
//}

//interface IReceiveJobAction {
//    type: 'RECEIVE_JOB';
//    job: IJob;
//}

//interface ISaveJob {
//    type: 'SAVE_JOB';
//    job: IJob;
//}

//interface IDeleteJob {
//    type: 'DELETE_JOB';
//}

//interface IOperationSuccessJob {
//    type: 'OPERATION_SUCCESS_JOB';
//}

//interface IOperationFailedJob {
//    type: 'OPERATION_FAILED_JOB';
//    payload: {}
//}

interface ICreateJob {
    type: 'CREATE_JOB';
    data: IJob;
}

interface ICreateJobSuccessAction {
    type: 'CREATE_JOB_SUCCESS'
}

interface IOperationFailedJobAction {
    type: 'OPERATION_FAILED_JOB';
    payload: IOperationFailedJob;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = ICreateJob | ICreateJobSuccessAction | IOperationFailedJobAction;