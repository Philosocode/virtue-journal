import React, { Component } from "react";
import { RouteComponentProps, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

import { AppState } from "../redux/store";
import { Entry, getEntriesForVirtue, getUncategorizedEntries, deleteEntry } from "../redux/entry";

import { EntryItem } from "../components/entry-item";
import { LinkButton } from "../components/shared/link-button";

interface Props extends RouteComponentProps {
  entries: Entry[],
  getEntriesForVirtue: (virtueId: number) => any,
  getUncategorizedEntries: () => any,
  deleteEntry: (entryId: number) => Promise<undefined>
}

interface RouteProps {
  virtueId?: string
}

class _EntriesPage extends Component<RouteComponentProps<RouteProps> & Props> {
  componentDidMount() {
    const virtueIdParam = this.props.match.params.virtueId;
    let virtueId: number | undefined;
    if (virtueIdParam) virtueId = Number.parseInt(virtueIdParam);

    this.setState({ virtueId: virtueIdParam });

    // Get entries for virtue
    virtueId
      ? this.props.getEntriesForVirtue(virtueId)
      : this.props.getUncategorizedEntries();
  }

  handleClick = (entryId: number) => {
    this.props.history.push(`/entries/${entryId}`);
  }

  handleEdit = (entryId: number) => {
    this.props.history.push(`/entries/${entryId}/edit`);
  }

  handleDelete = (entryId: number) => {
    this.props.deleteEntry(entryId);
  }

  getEntriesList = () => {
    const { entries } = this.props;
    let contentToRender;

    if (entries.length > 0) {
      contentToRender = (
        entries.map(e => (
          <EntryItem
            key={e.entryId}
            entryId={e.entryId}
            createdAt={e.createdAt}
            lastEdited={e.lastEdited}
            title={e.title}
            description={e.description}
            starred={e.starred}
            virtueLinks={e.virtueLinks}
            handleClick={this.handleClick}
            handleDelete={this.handleDelete}
            handleEdit={this.handleEdit}
          />
        )
      ))
    } else {
      contentToRender = <p>No entries to display... why don't you add one?</p>
    }

    return contentToRender;
  }

  render() {
    return (
      <div>
        <h1>Entries Page</h1>
        <LinkButton
          to="/entries/create"
          className="c-virtue__create-button"
        >Create Entry</LinkButton>
        { this.getEntriesList() }
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  entries: state.entry.entries
});

export const EntriesPage = withRouter(connect(
  mapStateToProps,
  { getEntriesForVirtue, getUncategorizedEntries, deleteEntry }
)(_EntriesPage));