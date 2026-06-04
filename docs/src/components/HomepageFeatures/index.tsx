import type {ReactNode} from 'react';
import clsx from 'clsx';
import Heading from '@theme/Heading';
import Link from '@docusaurus/Link';
import CodeBlock from '@theme/CodeBlock';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  description: ReactNode;
};

const FeatureList: FeatureItem[] = [
  {
    title: 'Pipeline-first',
    description: 'Every stage of analysis — data loading, preprocessing, method execution — flows through a single pipeline. No manual wiring.',
  },
  {
    title: 'Composable steps',
    description: 'Normalization, transformation, and importers are independent steps. Swap, reorder, or skip them without touching the rest.',
  },
  {
    title: 'Built for .NET',
    description: 'Fluent builder API, immutable configurations, stateless execution. Fits naturally into any .NET 8+ project.',
  },
];

function Feature({title, description}: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className={styles.featureCard}>
        <Heading as="h3" className={styles.featureTitle}>{title}</Heading>
        <p className={styles.featureDesc}>{description}</p>
      </div>
    </div>
  );
}

const codeExample = `new PipelineBuilder<double>()
    .ConfigureExecution(o => o.Precision = 4)
    .WithData(b => b
        .WithMatrix(matrix)
        .AddCriterion(c => c
          .WithType(CriterionType.Benefit)
          .WithWeight(0.5))
        .AddCriterion(c => c
          .WithType(CriterionType.Cost)
          .WithWeight(0.5)))
    .AddProcessingStep(new NormalizationStepBuilder<double>()
        .WithMethod(NormalizationMethod.MinMax))
    .Build()
    .Execute(new Vikor<double>());`;

export default function HomepageFeatures(): ReactNode {
  return (
    <>
      <section className={styles.features}>
        <div className="container">
          <div className="row">
            {FeatureList.map((props, idx) => (
              <Feature key={idx} {...props} />
            ))}
          </div>
        </div>
      </section>

      <section className={styles.codeSection}>
        <div className="container">
          <div className={styles.codeSectionInner}>
            <div className={styles.codeSectionText}>
              <Heading as="h2" className={styles.codeSectionTitle}>
                One pipeline.<br/>Full analysis.
              </Heading>
              <p className={styles.codeSectionDesc}>
                Define the process once. The pipeline handles data flow between every step — from raw input to a sorted ranking.
              </p>
              <Link className="button button--primary" to="/docs/usage/pipeline/overview">
                See Pipeline docs
              </Link>
            </div>
            <div className={styles.codeBlock}>
              <CodeBlock language="csharp">{codeExample}</CodeBlock>
            </div>
          </div>
        </div>
      </section>
    </>
  );
}
