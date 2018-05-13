import * as webpack from 'webpack';
import * as path from 'path';

const config: webpack.Configuration = {
  mode: 'development',
  devtool: 'inline-source-map',
  entry: [
    './app/index.ts',
  ],
  output: {
    filename: 'intevent.js',
    path: path.resolve(__dirname, 'wwwroot', 'js'),
  },
  /*
  plugins: [
  ],
  */
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },
  module: {
    rules: [
      {
        enforce: 'pre',
        test: /\.js$/,
        loader: 'source-map-loader',
      },
      {
        test: /\.tsx?$/,
        loaders: ['ts-loader'],
        sideEffects: false,
      },
      {
        test: /\.(png|svg|jpg|gif)$/,
        use: ['file-loader'],
      },
      {
        test: /\.(woff|woff2|eot|ttf|otf)$/,
        use: ['file-loader'],
      },
    ]
  },
  /*
  optimization: {
  },
  */
};

export default config;
