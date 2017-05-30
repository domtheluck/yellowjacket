import * as React from "react";
import { Router, Route, hashHistory , IndexRedirect } from "react-router";

import Home from "./components/views/Home";
import Agents from "./components/views/Agent/Agents"

import Jobs from "./components/views/Job/Jobs";
import JobForm from "./components/views/Job/JobForm";

import View2 from "./components/views/View2";

import { Main } from "./components/layouts/Main";
import Blank from "./components/layouts/Blank";

import NotFound from "./components/common/NotFound";

export default (
    <Router history={hashHistory}>
        <Route path="/" component={Main}>
            //<IndexRedirect to="/home" />
            <Route path="home" component={Home}></Route>
            <Route path="agents" component={Agents}></Route>
            <Route path="jobs" component={Jobs}></Route>
            <Route path="jobForm" component={JobForm}></Route>
            <Route path="*" component={NotFound} />
        </Route>
    </Router>
);

// Enable Hot Module Replacement (HMR)
if (module.hot) {
    module.hot.accept();
}
