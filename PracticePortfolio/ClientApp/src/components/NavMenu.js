import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);
        this.state = {
            isOpen: false
        };
    }

    openNav = () => {
        this.setState({ isOpen: true });
    };

    closeNav = () => {
        this.setState({ isOpen: false });
    };

    render() {
        const { isOpen } = this.state;

        return (
            <div>
                <div id="mySidenav" className={`sidenav ${isOpen ? 'open' : ''}`}>
                    <button className="closebtn" onClick={this.closeNav} class="close-nav-btn">&times;</button>
                    <Link to="/">Home</Link>
                    <Link to="/counter">Counter</Link>
                    <Link to="/fetch-data">Weather</Link>
                </div>

                <button onClick={this.openNav} class="hamburger-button">
                    <span class="hamburger-icon"></span>
                </button>

            </div>
        );
    }
}
