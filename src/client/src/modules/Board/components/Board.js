import React, { Component } from 'react';
import _ from "lodash";
import clsx from "clsx";
import { Grid, makeStyles, useTheme, useMediaQuery } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components";
import Section from "./Section";
import { TASK_STATUSES } from "../../Task/constants"
import { withSignIn } from '../../SignIn/components';
import { compose, withHandlers } from 'recompose';
import withLoader from '../../shared/withLoader';
import SideBar from './SideBar';
import { currentUserQuery } from '../queries';
import { Query } from 'react-apollo';
import { SectionTaskPreview } from './SectionTaskPreview';

const useStyles = makeStyles(theme => ({
    board: {
        display: "grid",
        gridTemplateRows: "calc(100vh - 68px)"
    },
    sectionsContainer: {
        display: "grid",
        gridTemplateColumns: "12px auto 12px",
        gridTemplateRows: "minmax(calc(100vh - 68px), 200px)",
        // height: "100%"
    },
    sectionsContainerShift: {}
}));

const styles = {
    sections: {
        display: "grid",
        gridAutoColumns: "minmax(350px, 1fr)",
        gridColumnGap: 8,
        gridAutoFlow: "column"
    },
    modalContainer: {
        top: 0,
        left: 0,
        position: "absolute",
        height: "100%",
        width: "100%"
    }
}

const withData = WrappedComponent => props => (
    <Query query={currentUserQuery} fetchPolicy="cache-only">{({ data }) => (
        <WrappedComponent {...props} currentUser={_.get(data, "currentUser", {})} />
    )}</Query>
);

const Board = props => {
    const { id } = props.match.params;
    const { isCreating, currentUser } = props;

    const classes = useStyles();
    const theme = useTheme();

    const menuOpened = _.get(currentUser, "menuOpened", false);
    const searchText = _.get(currentUser, "searchText", "");
    const searchUser = _.get(currentUser, "searchUser", "");

    return (
        <React.Fragment>
            <div />
            <Scrollbars className={classes.board}
                autoHide={true} >
                <div className={clsx(classes.sectionsContainer, {
                    [classes.sectionsContainerShift]: menuOpened
                })}>
                    <div />
                    <div style={styles.sections}>
                        {_.map(TASK_STATUSES, st => <Section status={st} searchText={searchText} searchUser={searchUser} />)}
                        <SectionTaskPreview />
                    </div>
                    <div />
                </div>
            </Scrollbars>

            {id || isCreating ? (
                <div style={styles.modalContainer}>
                    {id ? <UpdateTask id="modal" todoId={id} /> : null}
                    {isCreating ? <CreateTask /> : null}
                </div>
            ) : null}

        </React.Fragment>
    )
}

export default compose(
    withSignIn,
    // withLoader,
    withData
)(Board);