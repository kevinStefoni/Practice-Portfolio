import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import './Layout.css'

export class Layout extends Component {
    static displayName = Layout.name;
    static state = {
        isPushed: false
    };

    pushRight = () => {
        this.setState({ isPushed: true });
    };

    pullLeft = () => {
        this.setState({ isPushed: false });
    };

    render() {
        return (
            <div>
                <NavMenu />
                <Container tag="main" id="main">
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
