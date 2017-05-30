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

import * as React from "react";
import { Component } from "react";
import * as $ from "jquery";
import { Dropdown } from "react-bootstrap";
import { Router, Route, hashHistory, IndexRedirect, Link } from "react-router";

export class Navigation extends React.Component<any, any> {

    static contextTypes = { router: React.PropTypes.object }

    componentDidMount() {
        const { menu } = this.refs;
    }

    activeRoute(routeName) {
        return this.context.router.isActive({ pathname: routeName }) ? "active" : "";
    }

    secondLevelActive(routeName) {
        return this.context.router.isActive({ pathname: routeName }) ? "nav nav-second-level collapse in" : "nav nav-second-level collapse";
    }

    render() {
        return (
            <nav className="navbar-default navbar-static-side" role="navigation">
                <ul className="nav metismenu" id="side-menu" ref="menu">
                    <li className="nav-header">
                        <div className="dropdown profile-element"> <span>
                        </span>
                            <a data-toggle="dropdown" className="dropdown-toggle" href="#">
                                <span className="clear"> <span className="block m-t-xs"> <strong className="font-bold">Example user</strong>
                                </span> <span className="text-muted text-xs block">Example position<b className="caret"></b></span> </span> </a>
                            <ul className="dropdown-menu animated fadeInRight m-t-xs">
                                <li><a href="#"> Logout</a></li>
                            </ul>
                        </div>
                        <div className="logo-element">
                            YJ+
                        </div>
                    </li>
                    <li className={this.activeRoute("/home")}>
                        <Link to="/"><i className="fa fa-th-large"></i> <span className="nav-label">Home</span></Link>
                    </li>
                    <li className={this.activeRoute("/agents")}>
                        <Link to="/agents"><i className="fa fa-th-large"></i> <span className="nav-label">Agents</span></Link>
                    </li>
                    <li className={this.activeRoute("/jobs")}>
                        <Link to="/jobs"><i className="fa fa-th-large"></i> <span className="nav-label">Jobs</span></Link>
                    </li>
                    <li className={this.activeRoute("/jobForm")}>
                        <Link to="/jobForm"><i className="fa fa-th-large"></i> <span className="nav-label">JobForm</span>
                        </Link>
                    </li>
                </ul>
            </nav>
        );
    }
}

export default Navigation