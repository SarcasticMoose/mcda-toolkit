import type {ReactNode} from 'react';
import clsx from 'clsx';
import Heading from '@theme/Heading';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  description: ReactNode;
};

const FeatureList: FeatureItem[] = [
  {
    title: 'Easy to Use',
    description: (
      <>
          No complex setup or learning curve—just import and go
      </>
    ),
  },
  {
    title: 'Simple and Intuitive',
    description: (
      <>
          Built with a fluent interface that makes setup fast and readable. Easily configure decision-making data with minimal code and maximum clarity
      </>
    ),
  },
  {
    title: 'Powered by .NET',
    description: (
      <>
          Fully compatible with both .NET 6 and .NET Standard 2.0, ensuring support for a wide range of application
      </>
    ),
  },
];

function Feature({title, description}: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
      </div>
      <div className="text--center padding-horiz--md">
        <Heading as="h3">{title}</Heading>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures(): ReactNode {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
