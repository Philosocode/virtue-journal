import React, { Component } from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

// Components
import { Navbar } from './components/navbar.component';

// Pages
import { IndexPage } from "./pages/index.page";
import { RegisterPage } from "./pages/register.page";
import { LoginPage } from "./pages/login.page";
import { SettingsPage } from "./pages/settings.page";

import { VirtuesPage } from "./pages/virtues.page";
import { VirtueDetailPage } from "./pages/virtue-detail.page";
import { VirtueAddEditPage } from './pages/virtue-add-edit.page';

import { EntriesPage } from "./pages/entries.page";
import { EntryDetailPage } from "./pages/entry-detail.page";
import { EntryAddEditPage } from "./pages/entry-add-edit.page";

import { NotFoundPage } from "./pages/not-found.page";

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div className="o-site-layout">
          <Navbar />
          <Switch>
            <Route exact path="/" component={IndexPage} />
            <Route exact path="/register" component={RegisterPage} />
            <Route exact path="/login" component={LoginPage} />
            
            <Route exact path="/virtues" component={VirtuesPage} />
            <Route exact path="/virtues/add" component={VirtueAddEditPage} />
            <Route exact path="/virtues/:virtueId/details" component={VirtueDetailPage} />
            <Route exact path="/virtues/:virtueId/edit" component={VirtueAddEditPage} />
            <Route exact path="/virtues/:virtueId/entries" component={EntriesPage} />

            <Route exact path="/entries/:entryId" component={EntryDetailPage} />
            <Route exact path="/entries/:entryId/edit" component={EntryAddEditPage} />

            <Route exact path="/settings" component={SettingsPage} />
            <Route component={NotFoundPage} />
          </Switch>
        </div>
      </BrowserRouter>
    )
  }
};

export { App };