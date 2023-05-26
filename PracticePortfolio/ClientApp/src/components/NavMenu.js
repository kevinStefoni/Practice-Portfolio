import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    handleCloseNav = () => {
        this.props.onCloseNav();
    };

    render() {
        const { isPushed } = this.props;

        return (
            <div>
                <div id="mySidenav" className={`sidenav ${isPushed ? 'open' : ''}`}>
                    <button className="close-nav-btn" onClick={this.handleCloseNav}>
                        &times;
                    </button>
                    <Link to="/">Home</Link>
                    <Link to="/fetch-data">Weather</Link>
                    <Link to="/singleton">Singleton</Link>
                </div>

                <button onClick={this.props.onPushToggle} className="hamburger-button">
                    <span className="hamburger-icon"></span>
                </button>
            </div>
        );
    }
}
