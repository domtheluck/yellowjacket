import * as React from "react";
import { Router, Route, hashHistory , IndexRedirect } from "react-router";
import { Layout } from "./components/Layout";

import Home from "./components/views/Home";
import Agents from "./components/views/Agents"
import View1 from "./components/views/View1";
import View2 from "./components/views/View2";

//import FetchData from "./components/FetchData";
//import Counter from "./components/Counter";
import { Main } from "./components/layouts/Main";
import Blank from "./components/layouts/Blank";

//export default
//    <Route component={Main}>
//    <Route path="/" components={{ body: Home }} />
//</Route>;

export default (
    <Router history={hashHistory}>
        <Route path="/" component={Main}>
            //<IndexRedirect to="/home" />
            <Route path="home" component={Home}> </Route>
            <Route path="agents" component={Agents}></Route>
            <Route path="view1" component={View1}></Route>
            <Route path="view2" component={View2}></Route>
        </Route>
    </Router>
);

//export default <Route component={Main}>
//    <Route path="/" components={{ body: Home }} />
//    <Route path="/agents" components={{ body: Agents }} />
//    <Route path="/view1" components={{ body: View1 }} />
//    <Route path="/view2" components={{ body: View2 }} />
//</Route>;

// Enable Hot Module Replacement (HMR)
if (module.hot) {
    module.hot.accept();
}
