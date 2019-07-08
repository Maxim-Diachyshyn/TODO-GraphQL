import React, { Component } from 'react';
import _ from "lodash";
import { Grid } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components";
import Section from "./Section";
import { TASK_STATUSES } from "../../Task/constants"
import { withSignIn } from '../../SignIn/components';
import { compose } from 'recompose';
import withLoader from '../../shared/withLoader';

const styles = {
    scrollContainer: {
        display: "grid",
        gridTemplateRows: "calc(100vh - 68px)",
        marginTop: "64px"
    },
    sectionsContainer: {
        display: "grid",
        gridTemplateColumns: "12px auto 12px",
        gridTemplateRows: "minmax(calc(100vh - 68px), 200px)",
        // height: "100%"
    },
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

class Board extends Component {
    render() {
        const { id } = this.props.match.params;
        const { isCreating } = this.props;
        return (
            <React.Fragment>
                <Scrollbars style={styles.scrollContainer} autoHide={true} >
                    <div style={styles.sectionsContainer}>
                        <div />
                        <div style={styles.sections}>
                            {_.map(TASK_STATUSES, st => <Section status={st} />)}
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
}

export default compose(
    withSignIn,
    withLoader,
)(Board);