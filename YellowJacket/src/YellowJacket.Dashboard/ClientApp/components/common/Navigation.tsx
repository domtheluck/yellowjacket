import * as React from "react";
import { Component } from "react";
import * as $ from "jquery";
import { Dropdown } from "react-bootstrap";
import { Router, Route, hashHistory, IndexRedirect, Link } from "react-router";
//import * as MetisMenu from "react-metismenu";

export class Navigation extends React.Component<any, any> {

    static contextTypes = { router: React.PropTypes.object }

    componentDidMount() {
        const { menu } = this.refs;
        //$(menu).metisMenu();
        //
        //ReactDOM.render(<MetisMenu />, document.getElementById(this.refs));
        //<MetisMenu this.refs>

        //$(document).ready(() => {
        //$("#side-menu").metisMenu();
        //});
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
                    <li className={this.activeRoute("/view1")}>
                        <Link to="/view1"><i className="fa fa-th-large"></i> <span className="nav-label">view1</span></Link>
                    </li>
                    <li className={this.activeRoute("/view2")}>
                        <Link to="/view2"><i className="fa fa-th-large"></i> <span className="nav-label">view2</span>
                        </Link>
                    </li>
                </ul>
            </nav>
        );
    }
}

export default Navigation