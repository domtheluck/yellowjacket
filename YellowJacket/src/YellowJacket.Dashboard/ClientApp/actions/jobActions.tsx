import axios from "axios";

import { IResult } from "ClientApp/models/Models";

export class PostActions {
    // job list
    static readonly fetchJobs = "FETCH_JOBS";
    static readonly fetchJobsSuccess = "FETCH_POSTS_SUCCESS";
    static readonly fetchJobsFailure = "FETCH_JOBS_FAILURE";
    static readonly resetJobs = "RESET_POSTS";

    // create job
    static readonly createJob = "CREATE_JOB";
    static readonly createJobSuccess = "CREATE_JOB_SUCCESS";
    static readonly createJobFailure = "CREATE_JOB_FAILURE";
    static readonly resetNewJob = "RESET_NEW_JOB";

    // validate job fields on the server
    static readonly validateJobFields = "VALIDATE_JOB_FIELDS";
    static readonly validateJobFieldsSuccess = "VALIDATE_JOB_FIELDS_SUCCESS";
    static readonly validateJobFieldsFailure = "VALIDATE_JOB_FIELDS_FAILURE";
    static readonly resetJobFields = "RESET_JOB_FIELDS";

    // fetch job
    static readonly fetchJob = "FETCH_JOB";
    static readonly fetchJobSuccess = "FETCH_JOB_SUCCESS";
    static readonly fetchJobFailure = "FETCH_JOB_FAILURE";
    static readonly resetActiveJob = "RESET_ACTIVE_JOB";

    // delete job
    static readonly deleteJob = "DELETE_JOB";
    static readonly deleteJobSuccess = "DELETE_JOB_SUCCESS";
    static readonly deleteJobFailure = "DELETE_JOB_FAILURE";
    static readonly resetDeletedJob = "RESET_DELETED_JOB";
}

let fetchJobs = () => {
    const request = axios({
        method: "get",
        url: "/api/v1/jobs",
        headers: []
    });

    const result = {
        type: fetchJobs,
        payload: {}
    };

    axios.get("/api/v1/jobs")
        .then(response => {
            result.payload = response.data;
        })
        .catch(error => {
            console.log(error);
        });

    return result;
}