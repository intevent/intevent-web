import * as React from 'react';
import { withStyles, Theme, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import SkipPreviousIcon from '@material-ui/icons/SkipPrevious';
import PlayArrowIcon from '@material-ui/icons/PlayArrow';
import ThumbsUp from '@material-ui/icons/ThumbUp';
import ThumbsDown from '@material-ui/icons/ThumbDown';
const styles = (theme: Theme) =>
  createStyles({
    card: {
      display: 'flex'
    },
    details: {
      display: 'flex',
      flexDirection: 'column'
    },
    content: {
      flex: '1 0 auto'
    },
    cover: {
      width: 151,
      height: 151
    },
    controls: {
      display: 'flex',
      alignItems: 'center',
      paddingLeft: theme.spacing.unit,
      paddingBottom: theme.spacing.unit
    },
    playIcon: {
      height: 38,
      width: 38
    }
  });

function MediaControlCard(props) {
  const { classes, theme } = props;

  return (
    <div>
      <Card className={classes.card}>
        <div className={classes.details}>
          <CardContent className={classes.content}>
            <Typography variant="headline">Live From Space</Typography>
            <Typography variant="subheading" color="textSecondary">
              Mac Miller
            </Typography>
          </CardContent>
          <div className={classes.controls}>
            <IconButton aria-label="Previous">
              {theme.direction === 'rtl' ? <ThumbsUp /> : <ThumbsDown />}
            </IconButton>
            <IconButton aria-label="Next">
              {theme.direction === 'rtl' ? <ThumbsDown /> : <ThumbsUp />}
            </IconButton>
            <Typography variant="subheading" color="textSecondary">
              Votes: 0
            </Typography>
          </div>
        </div>
        <CardMedia
          className={classes.cover}
          image="/static/images/cards/live-from-space.jpg"
          title="Live from space album cover"
        />
      </Card>
    </div>
  );
}

export default withStyles(styles, { withTheme: true })(MediaControlCard);
