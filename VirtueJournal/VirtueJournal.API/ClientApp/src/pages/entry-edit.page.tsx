import React, { Component } from "react";
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';

import { editEntry, VirtueLink, EntryState, EntryForEdit } from "../redux/entry";
import { getVirtues, VirtueState } from "../redux/virtue";
import { AppState } from "../redux/store";
import { AxiosResponse, AxiosError } from "axios";
import { VirtueLinkList } from "../components/virtue-link-list";

interface Props extends RouteComponentProps {
  virtue: VirtueState,
  entry: EntryState,
  prevPath?: string,
  getVirtues: () => Promise<void>,
  editEntry: Function
}

interface RouteProps {
  entryId?: string
}

class _EntryEditPage extends Component<RouteComponentProps<RouteProps> & Props> {
  state = {
    entryId: null,
    title: "",
    description: "",
    createdAt: new Date().toISOString().substr(0, 10),
    starred: false,
    selectedVirtue: undefined,
    selectedDifficulty: 0,
    virtueLinks: [] as VirtueLink[],
    error: "",
    shouldRedirectToEntriesPage: false,
  };

  async componentDidMount() {
    const matchParams = this.props.match.params;

    if (!matchParams.entryId) {
      console.log("matchParams.entryId not found.");
      return;
    }

    const entryId = Number.parseInt(matchParams.entryId);
    const currentEntry = this.props.entry.entries.find(e => e.entryId === entryId);

    // Get user's virtues
    const { virtues } = this.props.virtue;

    if (virtues.length === 0) {
      await this.props.getVirtues();
    }

    if (!currentEntry) {
      console.log("No current entry...");
    } else {
      const createdAt = currentEntry.createdAt + "";

      this.setState({
        entryId: currentEntry.entryId,
        title: currentEntry.title,
        description: currentEntry.description,
        createdAt: new Date(Date.parse(createdAt)).toISOString().substr(0, 10),
        starred: currentEntry.starred,
        selectedVirtue: "DEFAULT",
        virtueLinks: currentEntry.virtueLinks,
      })
    }
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    this.setState({
      [event.target.name]: event.target.checked
    })
  }

  handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    // Convert option value to number
    this.setState({
      [event.target.name]: Number(event.target.value)
    });
  };

  handleSubmit = (event: React.SyntheticEvent) => {
    if (!this.state.entryId) return;

    event.preventDefault();

    const { title, description, starred, virtueLinks } = this.state;

    const createdAt = new Date(this.state.createdAt);
    const lastEdited = new Date();

    const editedEntry = { title, description, createdAt, lastEdited, starred, virtueLinks };

    this.props.editEntry(this.state.entryId, editedEntry)
      .then((res: AxiosResponse) => {
        this.props.history.goBack();
      })
      .catch((err: AxiosError) => {
        const errorResponse = err.response;

        if (errorResponse) {
          let errorString = `ERROR: ${errorResponse.data.title.slice(0, -1)}: `;

          Object.keys(errorResponse.data.errors).forEach((errorKey: string) => {
            errorString += ` ${errorKey}`;
          });

          this.setState({ error: errorString });
        }
      })
  }

  renderVirtueOptions = () => {
    const virtueLinkIds = this.state.virtueLinks.map(vl => vl.virtueId);
    const { virtues } = this.props.virtue;

    return virtues.map(v => { 
      if (virtueLinkIds.includes(v.virtueId)) return null;

      return <option key={v.virtueId} value={v.virtueId}>{v.name}</option>
    });
  }

  renderDifficultyOptions = () => {
    return (
      <>
        <option value="0">Very Easy</option>
        <option value="1">Easy</option>
        <option value="2">Medium</option>
        <option value="3">Hard</option>
        <option value="4">Very Hard</option>
      </>
    )
  }

  addVirtueLink = () => {
    const virtueId = this.state.selectedVirtue;
    if (!virtueId) return;

    const difficulty = this.state.selectedDifficulty;
    const newVirtueLink: VirtueLink = { virtueId, difficulty  }

    this.setState({ 
      virtueLinks: [...this.state.virtueLinks, newVirtueLink ],
      selectedDifficulty: 0,
      selectedVirtue: "DEFAULT",
    })
  }

  removeVirtueLink = (virtueId: number) => {
    const filteredVirtueLinks = this.state.virtueLinks.filter(vl => vl.virtueId !== virtueId);
    this.setState({ virtueLinks: filteredVirtueLinks });
  }

  render() {
    return (
      <div>
        <h1>Entry Edit Page</h1>
        <form onSubmit={this.handleSubmit}>
          <div className="form__group">
            <label htmlFor="title" className="form__label">Title:</label>
            <div className="form__input-container">
              <input
                required
                id="title"
                className="form__input"
                type="text"
                name="title"
                placeholder="Title"
                value={this.state.title}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="createdAt" className="form__label">Created At:</label>
            <div className="form__input-container">
              <input
                className="form__input"
                type="date"
                name="createdAt"
                id="createdAt"
                value={this.state.createdAt}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="starred" className="form__label">Starred:</label>
            <div className="form__input-container">
              <input
                className="form__input"
                type="checkbox"
                name="starred"
                id="starred"
                checked={this.state.starred}
                onChange={this.handleCheckboxChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="description" className="form__label">Description:</label>
            <div className="form__input-container">
              <textarea
                required
                id="description"
                className="form__input"
                name="description"
                placeholder="Description"
                value={this.state.description}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label className="form__label">Add Virtue Link:</label>
            <div className="form__input-container">
              <select
                id="selectedVirtue"
                name="selectedVirtue"
                value={this.state.selectedVirtue}
                onChange={this.handleSelectChange}
              >
                <option value="DEFAULT" disabled>Choose Virtue</option>
                { this.renderVirtueOptions() }
              </select>
              <select
                id="selectedDifficulty"
                name="selectedDifficulty"
                value={this.state.selectedDifficulty}
                onChange={this.handleSelectChange}
              >
                <option value="DEFAULT" disabled>Choose Difficulty</option>
                { this.renderDifficultyOptions() }
              </select>
              <button 
                type="button" 
                onClick={this.addVirtueLink} 
                disabled={this.state.selectedVirtue === "DEFAULT"}
              >Add Virtue Link</button>
            </div>
            { this.state.virtueLinks.length === 0 && <p>No virtue links...</p>}
            <VirtueLinkList 
              virtueLinks={this.state.virtueLinks} 
              canDelete
              handleDelete={this.removeVirtueLink} />
          </div>

          <button className="button" type="submit">Save Changes</button>
          <div className="error error--server">{this.state.error}</div>
        </form>
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  virtue: state.virtue,
  entry: state.entry
});

export const EntryEditPage = connect(
  mapStateToProps, 
  { editEntry, getVirtues }
)(_EntryEditPage);