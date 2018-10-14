import { HttpLink } from 'apollo-link-http';
import { WebSocketLink } from 'apollo-link-ws';

export const httpLink: HttpLink = new HttpLink({
  uri: 'http://localhost/graphql',
});

export const webSocketLink: WebSocketLink = new WebSocketLink({
  uri: `ws://localhost/graphql`,
  options: {
    reconnect: true,
  },
});
