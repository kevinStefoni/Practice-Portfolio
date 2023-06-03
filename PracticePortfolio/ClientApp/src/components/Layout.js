import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import './Layout.css';

export class Layout extends Component {
    state = {
        isPushed: false
    };

    handlePushToggle = () => {
        this.setState((prevState) => ({ isPushed: !prevState.isPushed }));
    };

    handleCloseNav = () => {
        this.setState({ isPushed: false });
    };

    render() {
        const { isPushed } = this.state;

        return (
            <div>
                <NavMenu
                    isPushed={isPushed}
                    onPushToggle={this.handlePushToggle}
                    onCloseNav={this.handleCloseNav}
                />
                <Container tag="main" id="main" style={{ marginLeft: isPushed ? '250px' : '2.25em' }}>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
