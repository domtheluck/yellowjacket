import * as React from "react";
import * as $ from "jquery";

import Progress from "../common/Progress";
import Navigation from "../common/Navigation";
import Footer from "../common/Footer";
import TopHeader from "../common/TopHeader";
import { correctHeight, detectBody } from "./Helpers";

export interface MainProps {
    body: React.ReactElement<any>;
}

export class Main extends React.Component<MainProps, void> {
    render() {
        //const wrapperClass = `gray-bg ${this.context.location.pathname}`;
        const wrapperClass = "gray-bg" + "active";
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