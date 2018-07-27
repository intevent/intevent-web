import * as webpack from 'webpack';
import * as path from 'path';
import { BundleAnalyzerPlugin } from 'webpack-bundle-analyzer';
const { InjectManifest } = require('workbox-webpack-plugin');

const appPath = path.resolve(__dirname, 'app');
const outputPath = path.resolve(__dirname, 'wwwroot', 'js');

const config: webpack.Configuration = {
  mode: 'development',
  devtool: 'inline-source-map',
  entry: {
    app: path.resolve(appPath, 'index.ts'),
  },
  output: {
    filename: '[name].intevent.js',
    chunkFilename: '[name].intevent.js',
    path: outputPath,
  },
  plugins: [
    new InjectManifest({
      importWorkboxFrom: 'cdn',
      swDest: path.resolve(outputPath, 'sw.intevent.js'),
      swSrc: path.resolve(appPath, 'sw.ts'),
    }),
    new BundleAnalyzerPlugin({
      analyzerMode: 'server',
      analyzerHost: '0.0.0.0',
      analyzerPort: 9000,
    }),
  ],
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
