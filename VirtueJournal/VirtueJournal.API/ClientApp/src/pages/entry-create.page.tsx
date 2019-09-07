import React, { Component } from "react";
import { Redirect } from 'react-router';
import { connect } from 'react-redux';

import { createEntry, VirtueLink } from "../redux/entry";
import { getVirtues, VirtueState } from "../redux/virtue";
import { AppState } from "../redux/store";
import { AxiosResponse, AxiosError } from "axios";

interface Props {
  virtue: VirtueState,
  createEntry: Function,
  getVirtues: () => Promise<void>
}

class _EntryCreatePage extends Component<Props> {
  state = {
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
    // Get user's virtues
    const { virtues, currentVirtue } = this.props.virtue;

    if (currentVirtue) {
      this.setState({ selectedVirtue: currentVirtue.virtueId });
    }

    if (virtues.length === 0) {
      await this.props.getVirtues();
      this.setState({ selectedVirtue: this.props.virtue.virtues[0].virtueId })
    }
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    // Convert option value to number
    this.setState({
      [event.target.name]: Number(event.target.value)
    });
  };

  handleSubmit = (event: React.SyntheticEvent) => {
    event.preventDefault();

    const { title, description, starred, virtueLinks } = this.state;

    const createdAt = new Date(this.state.createdAt);

    const entryToCreate = { title, description, createdAt, starred, virtueLinks };

    this.props.createEntry(entryToCreate)
      .then((res: AxiosResponse) => {
        this.setState({ shouldRedirectToEntriesPage: true });
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
      if (virtueLinkIds.includes(v.virtueId)) return;

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

  renderVirtueLinks = () => {
    return this.state.virtueLinks.map(vl => 
      <li className="c-virtue-link__item" key={vl.virtueId}>{vl.difficulty} - ID: {vl.virtueId}</li>
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

  render() {
    return (
      <div>
        { this.state.shouldRedirectToEntriesPage && <Redirect to="/virtues" /> }
        <h1>Entry Create Page</h1>
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
            <ul className="c-virtue-link__container">{ this.renderVirtueLinks() }</ul>
          </div>

          <button className="button" type="submit">Create Entry</button>
          <div className="error error--server">{this.state.error}</div>
        </form>
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  virtue: state.virtue
});

export const EntryCreatePage = connect(
  mapStateToProps, 
  { createEntry, getVirtues }
)(_EntryCreatePage);