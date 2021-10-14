# Proposal 2: Infra as Code

## Context
For Amped it is essential to have a properly Infra as Code configuration working along the code. This gives more insight for contributors how behind the scene the whole application is configured.

## Requirements
- Every infra contribution gets feedback as soon as possible
- Code deployments are mostly independent of Infra deployments
- Risk of exposure of credentials or other significant security risks are minimized
- The Infra code should explain itself by clearly describing functionality, parameters and variables
- Deployment done first on Azure

## Possible Solutions
1. Terraform
2. Bicep or ARM templates
3. ~~Pulumi~~ (out of scope for now)

## Proposed Implementation 1
My initial proposal would be to use Terraform with an automated workflow as facilitated by [a combination of Terraform Cloud and GitHub Actions](https://learn.hashicorp.com/tutorials/terraform/github-actions).

This facilitates basically a part of our CI/CD as proposed here ([docs](../1_cicd/cicd.md)/[PR](https://github.com/VXCompany/amped/pull/1)). The only difference here is that the implemenation is done completely in GitHub Actions, which has both advantages and disadvantages.

![](https://learn.hashicorp.com/img/terraform/automation/tfc-gh-actions-workflow.png)

### Advantages
- Ease of use
- Clear [examples](https://learn.hashicorp.com/tutorials/terraform/github-actions)
- GitHub centered workflow

### Disadvantages
- Use of Terraform Cloud (free plan)
- Plan in the open, which may have security implications

### Other directions within this approach
- www.runatlantis.io
- Azure DevOps (possibly securing access to Azure Cloud by using VMSS agents and MSI)

## Possible Implementation 2
Bicep as a language to define the infrastructure, ARM templates as a compiled version of this definitions. This approach is both executable on GitHub and Azure DevOps.

### Advantages
- Azure native technology
- New features available on release

### Disadvantages
- Limited to Azure
- No great feedback mechanism (`What-If` comes close, but is not as sophisticated as `terraform plan`)
