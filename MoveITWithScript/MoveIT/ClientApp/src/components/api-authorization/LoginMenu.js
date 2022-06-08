import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null,
            isAdmin : false,
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user.name, 
            isAdmin: user && user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "Administrator" 
        });
    }

    render() {
        const { isAuthenticated, userName, isAdmin } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath, isAdmin);
        }
    }

    authenticatedView(userName, profilePath, logoutPath, isAdmin) {
        return (<Fragment>
            {isAdmin && (
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/viewallofferslist">View all orders</NavLink>
                </NavItem>
            )}
            {!isAdmin && (
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/composeinquiry">Compose Inquiry</NavLink>
                </NavItem>
               
            )}
            {!isAdmin && (
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/viewofferlist">My offers</NavLink>
                </NavItem>
            )}
           
            {/*<NavItem>
                <NavLink tag={Link} className="text-dark" to={profilePath}>Hello {userName}</NavLink>
            </NavItem>*/}
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={logoutPath}>Logout</NavLink>
            </NavItem>
        </Fragment>);

    }

    anonymousView(registerPath, loginPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={registerPath}>Register</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={loginPath}>Login</NavLink>
            </NavItem>
        </Fragment>);
    }
}
