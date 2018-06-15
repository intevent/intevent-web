import * as webpack from 'webpack';
import * as path from 'path';

const config: webpack.Configuration = {
  mode: 'production',
  entry: [
    './app/index.ts',
  ],
  output: {
    filename: 'intevent.js',
    chunkFilename: '[name].intevent.js',
    path: path.resolve(__dirname, 'dist'),
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
  optimization: {
    splitChunks: {
      cacheGroups: {
        vendor: {
          test: /node_modules/,
          chunks: 'all',
          name: 'vendor',
          priority: 10,
          enforce: true,
        },
      },
    },
  },
};

export default config;
