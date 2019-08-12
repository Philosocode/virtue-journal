import React, { Component } from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import { IndexPage } from "./pages/index.page";
import { RegisterPage } from "./pages/register.page";
import { LoginPage } from "./pages/login.page";
import { SettingsPage } from "./pages/settings.page";

import { VirtuesPage } from "./pages/virtues.page";
import { VirtueDetailPage } from "./pages/virtue-detail.page";
import { EntriesPage } from "./pages/entries.page";
import { EntryDetailPage } from "./pages/entry-detail.page";

import { NotFoundPage } from "./pages/not-found.page";

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div className="o-site-layout">
          <Switch>
            <Route exact path="/" component={IndexPage} />
            <Route exact path="/register" component={RegisterPage} />
            <Route exact path="/login" component={LoginPage} />

            <Route exact path="/virtues" component={VirtuesPage} />
            <Route exact path="/virtues/:virtueId/details" component={VirtueDetailPage} />
            <Route exact path="/virtues/:virtueId/entries" component={EntriesPage} />
            <Route exact path="/entries/:entryId" component={EntryDetailPage} />

            <Route exact path="/settings" component={SettingsPage} />
            <Route component={NotFoundPage} />
          </Switch>
        </div>
      </BrowserRouter>
    )
  }
};

export { App };