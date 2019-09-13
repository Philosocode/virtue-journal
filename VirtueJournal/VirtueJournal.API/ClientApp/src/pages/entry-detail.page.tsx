import React, { Component } from "react";
import { connect } from 'react-redux';
import { RouteComponentProps } from "react-router-dom";
import { AxiosResponse, AxiosError } from "axios";

import { AppState } from "../redux/store";
import { getEntry, EntryState, VirtueLink } from "../redux/entry";
import { VirtueLinkList } from "../components/virtue-link-list";

/*
export interface Entry {
  entryId: number,
  title: string,
  description: string,
  createdAt: Date,
  lastEdited: Date,
  starred: boolean,
  virtueLinks?: VirtueLink[]
}
*/

interface RouteProps {
  entryId: string
}

interface Props extends RouteComponentProps<RouteProps> {
  entry: EntryState,
  getEntry: (entryId: number) => any
}

class _EntryDetailPage extends Component<Props> {
  componentDidMount() {
    const entryId = Number.parseInt(this.props.match.params.entryId);
    this.props.getEntry(entryId);
  }

  removeVirtueLink = (virtueId: number) => {
    console.log(virtueId);
  }

  render() {
    const currentEntry = this.props.entry.currentEntry;

    if (!currentEntry) return <div>Loading...</div>

    return (
      <div>
        <h1>{currentEntry.title}</h1>
        <p>Starred: {currentEntry.starred ? "true" : "false"}</p>
        <p>{currentEntry.description}</p>
        <hr/>
        <p>Created: {currentEntry.createdAt}</p>
        <p>Last Edited: {currentEntry.lastEdited || "never"}</p>
        {currentEntry.virtueLinks && <VirtueLinkList 
                                        virtueLinks={currentEntry.virtueLinks} 
                                        handleDelete={this.removeVirtueLink} 
                                      />
        }
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  entry: state.entry
});

export const EntryDetailPage = connect(
  mapStateToProps, 
  { getEntry }
)(_EntryDetailPage);