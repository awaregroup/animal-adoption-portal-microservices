import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Cart } from './components/Cart';
import { v4 as uuidv4 } from 'uuid';
import Cookies from 'js-cookie';

interface AppState {
    userName?: string;
    cartId: string;
}


export default class App extends Component<{}, AppState> {
    constructor(props: any) {
        super(props);
        this.state = {
            userName: this.retrieveUserName(),
            cartId: this.retrieveCartId()
        }
        this.handleLogout = this.handleLogout.bind(this);
    }

    componentDidMount() {
        //updates the z_name cookie based off of the location url
        let url = new URL(window.location.href);
        let name = url.searchParams.get("z_name") || undefined;

        if (name) {
            //updates the cookie
            Cookies.set('z_name', name);
            this.setState((state) => {
                return {
                    ...state,
                    userName: name
                }
            });
            //removes the query params
            window.history.replaceState({}, document.title, url.origin + url.pathname)
        }
    }


    handleLogin() {
        window.location.replace(`/api/login?redirectUrl=${window.location.href}`)
    }

    retrieveUserName() {
        return Cookies.get('z_name');
    }

    retrieveCartId() {
        let cartId = Cookies.get('z_cartId');
        if (!cartId) {
            let uuid = uuidv4();
            Cookies.set("z_cartId", uuid)
            return uuid;
        }
        return cartId;
    }

    handleLogout() {
        Cookies.remove("z_name");
        this.setState((state) => {
            return {
                ...state,
                userName: undefined
            }
        });
    }

    render() {
        return (
            <Layout handleLogin={this.handleLogin} handleLogout={this.handleLogout} userName={this.state.userName}>
                <Route exact path='/' component={Home} />
                <Route path='/cart' render={(props) => <Cart {...props} cartId={this.state.userName || this.state.cartId} />} />
            </Layout>
                );
            }
        }
