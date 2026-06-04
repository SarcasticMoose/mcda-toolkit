import React from 'react';
import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';
import CodeBlock from '@theme/CodeBlock';
import { useNugetVersion } from '@site/src/hooks/useNugetVersion';

interface NugetInstallTabsProps {
  packageName: string;
}

export default function NugetInstallTabs({ packageName }: NugetInstallTabsProps): React.JSX.Element {
  const version = useNugetVersion(packageName);

  return (
    <Tabs>
      <TabItem value="dotnet" label="Dotnet" default>
        <CodeBlock language="bash">{`dotnet add package ${packageName} --version ${version}`}</CodeBlock>
      </TabItem>
      <TabItem value="packagereference" label="PackageReference">
        <CodeBlock language="xml">{`<PackageReference Include="${packageName}" Version="${version}" />`}</CodeBlock>
      </TabItem>
      <TabItem value="packagemanager" label="PackageManager">
        <CodeBlock language="powershell">{`NuGet\\Install-Package ${packageName} -Version ${version}`}</CodeBlock>
      </TabItem>
    </Tabs>
  );
}
