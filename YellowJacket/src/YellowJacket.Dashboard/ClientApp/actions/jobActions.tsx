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

import axios from "axios";

// job list    
export const fetchJobsAction = "FETCH_JOBS";
export const fetchJobsSuccessAction = "FETCH_JOBS_SUCCESS";
export const fetchJobsFailureAction = "FETCH_JOBS_FAILURE";
export const resetJobsAction = "RESET_JOBS";

// create new job
export const createJobAction = "CREATE_JOBS";
export const createJobSuccessAction = "CREATE_JOBS_SUCCESS";
export const createJobFailureAction = "CREATE_JOBS_FAILURE";
export const resetNewJobAction = "RESET_NEW_JOBS";

// fetch job
export const fetchJobAction = "FETCH_JOB";
export const fetchJobSuccessAction = "FETCH_JOB_SUCCESS";
export const fetchJobFailureAction = "FETCH_JOB_FAILURE";
export const resetActiveJobAction = "RESET_ACTIVE_JOB";

// delete post
export const deleteJobAction = "DELETE_JOB";
export const deleteJobSuccessAction = "DELETE_JOB_SUCCESS";
export const deleteJobFailureAction = "DELETE_JOB_FAILURE";
export const resetDeletedJobAction = "RESET_DELETED_JOB";   

const baseUri = "api/v1";

export function fetchJobs() {
    const request = axios({
        method: "get",
        url: `${baseUri}/jobs`,
        headers: []
    });

    return {
        type: fetchJobs,
        payload: request
    };
};

export function fetchJobsSuccess(posts: any) {
    return {
        type: fetchJobsAction,
        payload: posts
    };
};

export function fetchJobsFailure(error: any) {
    return {
        type: fetchJobsFailureAction,
        payload: error
    };
};

export function createJob(props: any) {
    const request = axios({
        method: "post",
        data: props,
        url: `${baseUri}/jobs`,
        headers: []
    });

    return {
        type: createJobAction,
        payload: request
    };
};

export function createJobSuccess(newPost: any) {
    return {
        type: createJobSuccessAction,
        payload: newPost
    };
};

export function createJobFailure(error: any) {
    return {
        type: createJobFailureAction,
        payload: error
    };
};

export function resetNewJob() {
    return {
        type: resetNewJobAction
    }
};

export function resetDeletedJob() {
    return {
        type: resetDeletedJobAction
    }
};

export function fetchJob(id: string) {
    const request = axios.get(`${baseUri}/jobs/${id}`);

    return {
        type: fetchJobAction,
        payload: request
    };
};

export function fetchJobSuccess(activePost: any) {
    return {
        type: fetchJobSuccessAction,
        payload: activePost
    };
};

export function fetchJobFailure(error: any) {
    return {
        type: fetchJobFailureAction,
        payload: error
    };
};

export function resetActiveJob() {
    return {
        type: resetActiveJobAction
    }
};

export function deleteJob(id: string) {
    const request = axios({
        method: "delete",
        url: `${baseUri}/posts/${id}`,
        headers: []
    });
    return {
        type: deleteJobAction,
        payload: request
    };
};

export function deleteJobSuccess(deletedJob: any) {
    return {
        type: deleteJobSuccessAction,
        payload: deletedJob
    };
};

export function deleteJobFailure(response: any) {
    return {
        type: deleteJobFailureAction,
        payload: response
    };
};