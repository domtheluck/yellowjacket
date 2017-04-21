import * as React from "react";
import * as $ from "jquery";

import { Router, Route, hashHistory, IndexRedirect, Link } from "react-router";

import Progress from "../common/Progress";
import Navigation from "../common/Navigation";
import Footer from "../common/Footer";
import TopHeader from "../common/TopHeader";
import { correctHeight, detectBody } from "./Helpers";

export class Main extends React.Component<any, void> {
    render() {
        //const wrapperClass = `gray-bg ${this.context.location}`;
        const wrapperClass = "gray-bg skin-3";
        return (
            <div id="wrapper">
                <Progress />
                <Navigation location={this.context.location} />
                <div id="page-wrapper" className={wrapperClass}>
                    <TopHeader />
                    {this.props.children}
                    <Footer />
                </div>
            </div>
        );
    };

    componentDidMount() {
        // Run correctHeight function on load and resize window event
        $(window).bind("load resize", () => {
            correctHeight();
            detectBody();
        });

        // Correct height of wrapper after metisMenu animation.
        $(".metismenu a").click(() => {
            setTimeout(() => { correctHeight(); }, 300);
        });
    };
}